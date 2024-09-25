using UnityEngine;

namespace Watermelon
{
    public class AnimalCarryingStateBehaviour : AnimalStateBehaviour
    {
        public AnimalCarryingStateBehaviour(AnimalStateMachineController stateMachineController) : base(stateMachineController)
        {

        }

        public override void OnStateRegistered()
        {

        }

        public override void OnStateActivated()
        {

        }

        public override void OnStateDisabled()
        {

        }

        public override void Update()
        {
          
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(PhysicsHelper.TAG_PLAYER))
            {
                IAnimalCarrying carrying = other.GetComponentInParent<IAnimalCarrying>();
                if (carrying != null)
                {
                    if (carrying.IsAnimalPickupAllowed())
                    {
                        // Create waiting indicator
                        stateMachineController.ParentBehaviour.CreateWaitingIndicator(carrying);
                    }
                    else
                    {
                        stateMachineController.ParentBehaviour.CreateWaitingIndicator(carrying, false);
                    }
                }
            }
        }

        public override void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(PhysicsHelper.TAG_PLAYER))
            {
                // Destroy waiting indicator
                stateMachineController.ParentBehaviour.DestroyWaitingIndicator();
            }
        }
    }
}