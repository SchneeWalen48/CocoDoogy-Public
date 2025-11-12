
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseUITest : MonoBehaviour
{
    //public Button testLoginButton_Anonymous;

    public TMP_InputField control_InputField;
    public TMP_Dropdown control_dropdown;

    public async void OnAnonymousLoginButtonClick()
    {
        if (!FirebaseManager.Instance && !FirebaseManager.Instance.IsInitialized) return;
        await FirebaseManager.Instance.SignInAnonymouslyTest((x) => Debug.Log($"파이어베이스 로그인 테스트: [성공]{x.UserId}로 로그인 성공"), x => Debug.LogWarning($"파이어베이스 로그인 테스트: [실패] - {x.Message}"));
        
    }

    public void UpdateTest()
    {
        if (!FirebaseManager.Instance && !FirebaseManager.Instance.IsInitialized) return;
        if (UserData.Local == null) return;
        UserData.OnLocalUserDataUpdate();
    }

    public void ControlUserData()
    {
        switch (control_dropdown.value)
        {
            //인벤주작
            case 0:
                if (int.TryParse(control_InputField.text, out int inventoryValue))
                {

                    if (UserData.Local.inventory.items.TryAdd(inventoryValue, 0))
                    {
                        UserData.Local.inventory.items[inventoryValue]++;
                    }
                    else
                    {
                        UserData.Local.inventory.items[inventoryValue]++;
                    }
                }
                else
                {
                    Debug.LogWarning("값을 조작할 곳과 넣을 값 잘 확인해야 함");
                }
                    break;

                //배치물주작
            case 1:
                if (int.TryParse(control_InputField.text, out int propValue))
                {

                    if (UserData.Local.lobby.props.TryAdd(propValue, new() { new() { xPosition = 1, yPosition = 2, yAxisRotation = 1 } }))
                    {
                        
                    }
                    else
                    {
                        UserData.Local.lobby.props[propValue][0] = new() { xPosition = int.MaxValue, yPosition = int.MaxValue, yAxisRotation = 270};
                    }
                }
                else
                {
                    Debug.LogWarning("값을 조작할 곳과 넣을 값 잘 확인해야 함");
                }
                break;

                //이벤트기록주작
            case 2:
                break;

                //친구추가주작
            case 3:
                break;
        }
    }

}
