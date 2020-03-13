using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{
    public sealed class PhysicsService : Service
    {
        #region Fields

        private const int COLLIDED_OBJECT_SIZE = 20;

        private readonly Collider2D[] _collidedObjects;
        private readonly RaycastHit2D[] _castBuffer;
        private readonly List<IOnTrigger> _triggeredObjects;
        private readonly GameContext _context;

        #endregion


        #region ClassLifeCycles

        public PhysicsService(Contexts contexts) : base(contexts)
        {
            _collidedObjects = new Collider2D[COLLIDED_OBJECT_SIZE];
            _castBuffer = new RaycastHit2D[64];
            _triggeredObjects = new List<IOnTrigger>();
        }

        #endregion

        
        #region Methods

        public bool CheckGround(Vector2 position, float distanceRay, out Vector2 hitPoint, int layerMask = LayerManager.DEFAULTLAYER)
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
        
        public bool Detection()
        {
            var enemy = _context.Enemy;
            RaycastHit2D hit = Physics2D.Raycast(enemy.Transform.position, enemy.Direction,enemy.detectionDistance);
            if(hit != false)
            {
                CustomDebug.Log("Defeat");
            }
            return true;
        }

        public List<IOnTrigger> GetObjectsInRadius(Vector2 position, float radius, int layerMask = LayerManager.DEFAULTLAYER)
        {
            _triggeredObjects.Clear();
            IOnTrigger trigger;

            int collidersCount = Physics2D.OverlapCircleNonAlloc(position, radius, _collidedObjects, layerMask);
            
            for (int i = 0; i < collidersCount; i++)
            {
                trigger = _collidedObjects[i].GetComponent<IOnTrigger>();

                if (trigger != null && !_triggeredObjects.Contains(trigger))
                {
                    _triggeredObjects.Add(trigger);
                }
            }

            return _triggeredObjects;
        }
        
        public HashSet<IOnTrigger> SphereCastObject(Vector2 center, float radius, HashSet<IOnTrigger> outBuffer,
            int layerMask = LayerManager.DEFAULTLAYER)
        {
            outBuffer.Clear();

            int hitCount = Physics2D.CircleCastNonAlloc(center,
                radius,
                Vector2.zero,
                _castBuffer,
                0.0f,
                layerMask);

            for (int i = 0; i < hitCount; i++)
            {
                IOnTrigger carTriggerProvider = _castBuffer[i].collider.GetComponent<IOnTrigger>();
                if (carTriggerProvider != null)
                {
                    outBuffer.Add(carTriggerProvider);
                }
            }


            return outBuffer;
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
