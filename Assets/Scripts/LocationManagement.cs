using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManagement : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start() {
        // 만약 유저가 gps 기능을 사용하지 않는다면 종료
        if(!Input.location.isEnabledByUser) {
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
            print("Time Out: " + Input.location.status);
            yield break;
        }

        // 만약 대기 시간 내에 시작되었으나, 실패한 경우에도 종료
        if(Input.location.status == LocationServiceStatus.Failed) {
            print("Unable to determine device location");
            yield break;
        }

        // 만약 대기 시간 내에 시작하는 데에 성공했다면, 위도, 경도, 고도, 정확도, 정보를 얻은 시간 출력
        Debug.Log("Location: " + Input.location.lastData.latitude +
            " " + Input.location.lastData.longitude +
            " " + Input.location.lastData.altitude +
            " " + Input.location.lastData.horizontalAccuracy +
            " " + Input.location.lastData.timestamp);

        // 위치 기능 종료
        Input.location.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
