using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrimino : MonoBehaviour
{

    
    //‰ñ“]‚µ‚Ä‚¢‚¢ƒuƒƒbƒN‚©‚Ç‚¤‚©
    [SerializeField] private bool canRotate = true;
    public int direction = 0;
    //•ûŠp@0 = –k,1=“Œ,2“ì, 3=¼


    


    //ˆÚ“®—p
    void Move(Vector3 moveDirection)//“®‚­•ûŒü‚ğ‚à‚ç‚¤
    {
        transform.position += moveDirection;
    }

    //ˆÚ“®ŠÖ”‚ğŒÄ‚ÔŠÖ”(4í—Ş)
    public void MoveLeft()
    {
        Move(new Vector3(-1, 0, 0));
    }
    public void MoveRight()
    {
        Move(new Vector3(1, 0, 0));
    }
    public void MoveUp()
    {
        Move(new Vector3(0, 1, 0));
    }
    public void MoveDown()
    {
        Move(new Vector3(0, -1, 0));
    }
    //‰ñ“]—p(2í—Ş)
    public void RotateRight()
    {
        //ƒ~ƒm‚Ìí—Ş‚¾‚¯‰ñ“]‚Ìí—Ş‚ª‚ ‚é
        if (canRotate)
        {
            transform.Rotate(0, 0, -90);
        }
        
    }
    public void RotateLeft()
    {
        if (canRotate)
        {
            transform.Rotate(0, 0, 90);
        }

    }

}
