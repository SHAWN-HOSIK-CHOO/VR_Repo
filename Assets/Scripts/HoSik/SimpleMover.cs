using UnityEngine;

namespace HoSik
{
   public abstract class SimpleMover : MonoBehaviour
   {
      public float speed    = 5f;  
      public float duration = 10f; 

      protected float _elapsedTime   = 0f;   
      protected bool  _movingForward = true;

      public virtual void MoveAttribute()
      {
         _elapsedTime += Time.deltaTime;
            
         if (_elapsedTime >= duration)
         {
            _movingForward = !_movingForward;
            _elapsedTime   = 0f; 
         }
            
         Vector3 direction = _movingForward ? transform.forward : -transform.forward; 
         transform.Translate(direction * (speed * Time.deltaTime), Space.World);
      }
   }
}
