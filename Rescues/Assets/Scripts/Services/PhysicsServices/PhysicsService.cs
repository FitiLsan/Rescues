using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{
    public sealed class PhysicsService: Service
    {
        public const int CollidedObjectSize = 20;

        Collider2D[] collidedObjects;

        public PhysicsService(Contexts contexts) : base(contexts)
        {
            collidedObjects = new Collider2D[CollidedObjectSize];
        }



        #region Methods

        public bool CheckGround(Vector2 position, float distanceRay, out Vector2 hitPoint, int layerMask = Physics.DefaultRaycastLayers)
        {
            hitPoint = Vector2.zero;

            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, distanceRay, layerMask);
            if (hit.collider == null)
            {
                return false;
            }

            hitPoint = hit.point;
            return true;
        }


        public List<IOnTrigger> GetObjectsInRadius(Vector2 position, float radius, int layerMask = Physics.DefaultRaycastLayers)
        {
            List<IOnTrigger> triggeredObjects = new List<IOnTrigger>();
            IOnTrigger trigger;

            int collidersCount = Physics2D.OverlapCircleNonAlloc(position, radius, collidedObjects, layerMask);


            for (int i = 0; i < collidersCount; i++)
            {
                trigger = collidedObjects[i].GetComponent<IOnTrigger>();

                if (trigger != null && !triggeredObjects.Contains(trigger))
                {
                    triggeredObjects.Add(trigger);
                }
            }

            return triggeredObjects;
        }


        public IOnTrigger GetNearestObject(Vector3 targetPosition, HashSet<IOnTrigger> objectBuffer)
        {
            float nearestDistance = Mathf.Infinity;
            IOnTrigger result = null;

            foreach (IOnTrigger trigger in objectBuffer)
            {
                float objectDistance = (targetPosition - trigger.GameObject.transform.position).sqrMagnitude;
                if (objectDistance >= nearestDistance)
                {
                    continue;
                }

                nearestDistance = objectDistance;
                result = trigger;
            }

            return result;
        }

        #endregion
    }
}
