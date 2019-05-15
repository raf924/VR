using UnityEngine;

    /// <summary>
    /// This componetn must be put on a little sphere (for instance) 
    /// and is used as a mouse cursor and keyboard event sender to the VncScreen
    /// 
    /// </summary>
    public class VNCmouseCursor : MonoBehaviour
    {
        private Ray ray;
        private RaycastHit hit;

        private Vector3 hit_pos;
        private Vector3 uvPos;


        private VNCScreen.VNCScreen vnc;
        private Collider touchedCollider = null;
        private Renderer r;

        public GameObject screen; 
        public bool manageKeys;

        void Awake()
        {
            r = GetComponent<Renderer>();
            // obj = GameObject.FindGameObjectWithTag("pointer");
        }


        /// <summary>
        /// Get the mosue position, send a raycast and identify the vnc screen uder mouse cursor
        /// Then, if any, send mouse event to it
        /// </summary>
        void Update()
        {

            ray = new Ray(Camera.main.transform.position, Camera.
        main.transform.forward);

        if (Physics.Raycast(ray, out hit, 1000))
            {
                Collider c = hit.collider;
                if (touchedCollider != c)
                {
                    touchedCollider = c;
                    vnc = c.GetComponent<VNCScreen.VNCScreen>();
                }
            }
            else
            {
                touchedCollider = null;
                vnc = null;
            }

            if (vnc != null)
            {
                hit_pos = hit.point;

                transform.position = hit_pos;
                uvPos = hit.textureCoord2;

                Debug.Log(uvPos);
                vnc.UpdateMouse(uvPos, false, false, false);
            }
        }

        /// <summary>
        /// Send key event to the active VncClient 
        /// 
        /// I use OnGUI because it is the simpliest way to get key event
        /// there are still many troubles, the shifts keys are not getted and many keys onb my keyboard are mishandled.
        /// 
        /// </summary>
        void OnGUI()
        {
            if (!manageKeys || vnc == null)
            {
                return;
            }
            Event theEvent = Event.current;

            if (theEvent.isKey)
            {
                if (theEvent.type == EventType.KeyDown)
                {
                    if (theEvent.keyCode != KeyCode.None)

                        vnc.OnKey(theEvent.keyCode, true);
                    //   Debug.Log("Down : " + theEvent.keyCode);

                }
                else if (theEvent.type == EventType.KeyUp)
                {
                    if (theEvent.keyCode != KeyCode.None)
                        vnc.OnKey(theEvent.keyCode, false);
                    // Debug.Log("Up : " + theEvent.keyCode);
                }
            }
        }


    }