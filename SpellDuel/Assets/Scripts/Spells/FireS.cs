using UnityEngine;

public class FireS : Spell,IControllable,IShootable
{
        public static string[] definition = {"Fire</color></b>: casts a flame controllable with your wand.","Firing</color></b>: Increased size and damage.","Fired</color></b>: Explodes when colliding or manually by left-pressing your wand."};
        private Timers tim;

        
        private void Start()
        {
                //VFX = Resources.Load("PS_FireBall") as GameObject;
                ImpactVFX = null;
                lifespan = 3f;
                speed = 8f;
                PM = 0.6f; //Power Multiplier
                cost = 2f;
                Element = Elements.Fire;
                ActiveCol = "#ff0000ff";
                InactiveCol = "#800000ff";
                tim = new Timers(1);

        }

        private void Update()
        {
                if (!IsOwner) return;
                tim.alarm[0] = tim.Timer(lifespan, tim.alarm[0], Debugger);
                /*if (_conjure == null) return;*/
                Control();
                Shoot();
        }

        public void Control()
        {
                //transform.forward = _conjure.ori.forward;
                var hitPoint = _conjure.aimAt.pointer;
                var direction = hitPoint.position - transform.position;
                Quaternion rot = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
        }

        public void Shoot()
        {
                transform.Translate(speed * Time.deltaTime * Vector3.forward);
        }

        private void OnTriggerEnter(Collider other)
        {
                //if (!IsOwner) return;
                Damager(other);  
        }

        private void Debugger()
        {
                if (!IsOwner) return;
                Despawner();
                Debug.Log("Tim is working hard");
        }

}
