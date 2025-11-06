using UnityEngine;

// This script's only job is to start the battle when the battle scene loads
public class BattleSceneController : MonoBehaviour
{
    [SerializeField] private BattleManager _battleManager;

    private void Start()
    {
        if (_battleManager == null)
        {
            Debug.LogError("BattleManager is not assigned in the BattleSceneController");
            return;
        }

        GameManager.Instance.StartBattle(_battleManager);
    }
}