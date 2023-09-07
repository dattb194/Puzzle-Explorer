using UnityEngine;

[System.Serializable]
public class PlayerMovement
{
    public Transform trans;
    public PlayerMovement(Transform trans)
    {
        this.trans = trans;
        previousPosition = trans.position;
    }

    // Khai báo một biến để lưu trữ vị trí cũ của player
    private Vector3 previousPosition;

    // Khai báo một biến để lưu trữ trạng thái di chuyển của player
    public bool isMoving;


    // Hàm này được gọi mỗi khung hình
    float timeCheck = 0;
    public void Update()
    {
        if (Vector3.Distance(trans.position, previousPosition) > .1f)
        {
            // Nếu có, thì gán trạng thái di chuyển là true
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (timeCheck <= 0)
        {
            timeCheck = 1;
            previousPosition = trans.position;
        }
        else
        {
            timeCheck -= Time.deltaTime;
        }
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
