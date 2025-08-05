using Steamworks;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
      // 싱글턴

    /// <summary> 
      /// 지정한 업적 ID에 해당하는 도전과제를 해제
    /// </summary>
    public void UnlockAchievement(EAchievementID achievementId)
    {
        // Steam API가 초기화되어 있어야 진행
        if (SteamManager.Initialized)
        {
            bool achieved;

                  // 해당 도전과제가 이미 해제되었는지 확인
            SteamUserStats.GetAchievement(achievementId.ToString(), out achieved);

                   // 이미 해제된 경우 함수 종료
            if (achieved)
                return; 

                  // 도전과제 해제 처리
            SteamUserStats.SetAchievement(achievementId.ToString());

                  // 변경된 정보를 Steam 서버에 저장
            SteamUserStats.StoreStats();
        }
    }

    /// <summary>
      /// 통계는 항상 갱신되어 진행도를 스팀 클라이언트에 표시
    /// </summary>
    public void AddStatAndCheckAchievement(EStatID statId, int amountToAdd, EAchievementID achievementId, int targetValue)
    {
            // Steam API가 초기화되어 있어야 진행
        if (!SteamManager.Initialized)
            return;

        int currentValue = 0;
            // 현재 통계 값을 가져옴
        SteamUserStats.GetStat(statId.ToString(), out currentValue);

            // 통계 값 갱신
        currentValue += amountToAdd;
        SteamUserStats.SetStat(statId.ToString(), currentValue);

            // 갱신된 통계 값을 Steam 서버에 저장 (진행도 표시용)
        SteamUserStats.StoreStats();

             // 목표값 도달 여부 체크
        if (currentValue >= targetValue)
        {
            bool achieved = false;

                  // 도전과제가 이미 해제되었는지 확인
            SteamUserStats.GetAchievement(achievementId.ToString(), out achieved);
            if (achieved)
                return;
                        
                  // 도전과제 해제 처리
            SteamUserStats.SetAchievement(achievementId.ToString());
            SteamUserStats.StoreStats();
        }
    }
}