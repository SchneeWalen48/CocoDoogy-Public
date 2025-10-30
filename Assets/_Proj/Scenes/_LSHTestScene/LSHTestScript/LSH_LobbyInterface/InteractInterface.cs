using UnityEngine;

// ����ڰ� ������Ʈ�� ��ȣ�ۿ�
public interface ILobbyInteractable
{
    void OnLobbyInteract();
}

public interface ILobbyDraggable
{
    void OnLobbyBeginDrag(Vector3 position);
    void OnLobbyDrag(Vector3 position);
    void OnLobbyEndDrag(Vector3 position);
}

public interface ILobbyPressable
{
    void OnLobbyPress();
}
//

public interface ILobbyCharactersEmotion
{
    void OnCocoMasterEmotion();
    void OnCocoAnimalEmotion();
}
