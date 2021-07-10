using UnityEngine;

public class Camera1 : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(2, 10)]
    public float smoothFactor;
    public Transform bg;
    public float length, startpos, screenlength, screenheight, startposy, lengthy;
    //private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        screenlength = Camera.main.GetComponent<Camera>().orthographicSize * Screen.width / Screen.height;
        screenheight = Camera.main.GetComponent<Camera>().orthographicSize;
        

    }
    private void LateUpdate()
    {
        length = bg.GetComponent<Renderer>().bounds.size.x;
        lengthy = bg.GetComponent<Renderer>().bounds.size.y;
        startpos = bg.position.x;
        startposy = bg.position.y;
        Follow();


    }

    private void Follow()
    {

       

        Vector3 xposistion = new Vector3(transform.position.x, target.transform.position.y, 0);
        Vector3 yposistion = new Vector3(target.transform.position.x, transform.position.y, 0);
        Vector3 dposition = new Vector3(transform.position.x, transform.position.y, 0);
        if (target.transform.position.x >= startpos + ((length / 2) - (screenlength)))
        {

            if (target.transform.position.y >= startposy + ((lengthy / 2) - (screenheight))) transform.position = dposition + offset;
            else if (target.transform.position.y <= startposy - ((lengthy / 2) - (screenheight))) transform.position = dposition + offset;
            else
            {
                transform.position = xposistion + offset;
            }
        }
        else if (target.transform.position.x <= startpos - ((length / 2) - (screenlength)))
        {
            if (target.transform.position.y >= startposy + ((lengthy / 2) - (screenheight))) transform.position = dposition + offset;
            else if (target.transform.position.y <= startposy - ((lengthy / 2) - (screenheight))) transform.position = dposition + offset;
            else
            {
                transform.position = xposistion + offset;
            }
        }
        else
        {
            if (target.transform.position.y >= startposy + ((lengthy / 2) - (screenheight))) transform.position = yposistion + offset;
            else if (target.transform.position.y <= startposy - ((lengthy / 2) - (screenheight))) transform.position = yposistion + offset;
            else
            {
                

                Vector3 targetPosition = target.position + offset;
                Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
                //Vector3 smoothPosition2 = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothFactor * Time.fixedDeltaTime);
                //Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition,  1.0f);
                transform.position = targetPosition;
            }

        }
       
    }
  
}
