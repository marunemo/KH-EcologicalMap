﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationManagement : MonoBehaviour {

    public GameObject LocationText = null;

    private bool gpsStarted = false;

    // Start is called before the first frame update
    IEnumerator Start() {
        // 만약 유저가 gps 기능을 사용하지 않는다면 종료
        if(!Input.location.isEnabledByUser) {
            Debug.Log("Gps permission is denied");
            yield break;
        }

        // gps 위치 기능 시작
        Input.location.Start();

        // 위치 서비스 기능이 시작될 때까지 대기(최대 20초)
        int maxWait = 20;
        while(Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // 만약 대기 시간이 내에 시작되지 못했다면 종료
        if(maxWait <= 0) {
            Debug.Log("Time Out: " + Input.location.status);
            yield break;
        }

        // 만약 대기 시간 내에 시작되었으나, 실패한 경우에도 종료
        if(Input.location.status == LocationServiceStatus.Failed) {
            Debug.Log("Unable to determine device location");
            yield break;
        }

        // 만약 대기 시간 내에 시작하는 데에 성공했다면, gps 위치 기능 시작
        gpsStarted = true;
    }

    // Update is called once per frame
    void Update() {
        // gps 기능이 시작되었따면, 위도, 경도, 고도, 정확도, 정보를 얻은 시간을 각각 출력
        if(gpsStarted) {
            LocationText.GetComponent<Text>().text = string.Format(
                "<b>Latitude</b> : {0:F6}\n<b>Longitude</b> : {1:F6}",
                Input.location.lastData.latitude, Input.location.lastData.longitude
            );

            /*
            Debug.Log("Location: " + Input.location.lastData.latitude +
                " " + Input.location.lastData.longitude +
                " " + Input.location.lastData.altitude +
                " " + Input.location.lastData.horizontalAccuracy +
                " " + Input.location.lastData.timestamp);
            */
        }
    }

    // Destroy is called before the object is removed
    void OnDestroy() {
        // gps 위치 기능이 켜져있다면, 해당 오브젝트 삭제 시에 종료
        if(gpsStarted) {
            Input.location.Stop();
        }
    }

    public static float getDistance(float lat, float lng) {
        // Haversine formula
        // [ref] https://en.wikipedia.org/wiki/Haversine_formula
        float earthRadius = 6371f;

        float lat1 = Input.location.lastData.latitude * Mathf.Deg2Rad;
        float lng1 = Input.location.lastData.longitude * Mathf.Deg2Rad;

        float lat2 = lat * Mathf.Deg2Rad;
        float lng2 = lng * Mathf.Deg2Rad;

        float deltaLat = lat2 - lat1;
        float deltaLng = lng2 - lng1;

        float hav = Mathf.Sin(deltaLat / 2) * Mathf.Sin(deltaLat / 2)
            + Mathf.Cos(lat1) * Mathf.Cos(lat2) * Mathf.Sin(deltaLng / 2) * Mathf.Sin(deltaLng / 2);
        float dist = 2 * earthRadius * Mathf.Atan2(Mathf.Sqrt(hav), Mathf.Sqrt(1 - hav));

        return dist;
    }
}
