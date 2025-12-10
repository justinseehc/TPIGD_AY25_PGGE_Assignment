using UnityEngine;

namespace PGGE
{
    // The base class for all third-person camera controllers
    public abstract class TPCBase
    {
        protected Transform mCameraTransform;
        protected Transform mPlayerTransform;

        public Transform CameraTransform
        {
            get
            {
                return mCameraTransform;
            }
        }
        public Transform PlayerTransform
        {
            get
            {
                return mPlayerTransform;
            }
        }

        public TPCBase(Transform cameraTransform, Transform playerTransform)
        {
            mCameraTransform = cameraTransform;
            mPlayerTransform = playerTransform;
        }

        public void RepositionCamera()
        {
            //-------------------------------------------------------------------
            // Implement here.
            //-------------------------------------------------------------------
            //-------------------------------------------------------------------
            // Hints:
            //-------------------------------------------------------------------
            // check collision between camera and the player.
            // find the nearest collision point to the player
            // shift the camera position to the nearest intersected point
            //-------------------------------------------------------------------

            // For Question 1 in Assignment

            ////- LECTURER GIVEN CODE ----------------------------------------------------------
            //// Define the layer mask for objects that might block the camera
            //LayerMask collisionMask = LayerMask.GetMask("Wall"); // Adjust this as needed

            //// Set the desired height for the camera above the player
            //float cameraHeight = 2.0f; // Adjust as per your character's height
            //Vector3 rayOrigin = mPlayerTransform.position + Vector3.up * cameraHeight;

            //// Calculate the direction and distance between camera and the player
            //Vector3 direction = mCameraTransform.position - rayOrigin;
            //float distance = direction.magnitude;

            //// Perform a raycast to detect objects between the camera and the player
            //RaycastHit hit;
            //if (Physics.Raycast(rayOrigin, direction.normalized, out hit, distance, collisionMask))
            //{
            //    // Adjust the camera position to the point of collision
            //    Vector3 collisionPoint = hit.point - direction.normalized * 0.1f; // Add a small offset
            //    mCameraTransform.position = new Vector3(
            //        collisionPoint.x,
            //        Mathf.Max(collisionPoint.y, rayOrigin.y),
            //        collisionPoint.z
            //    );
            //} else
            //{
            //    // Maintain the camera's desired height if no collision is detected
            //    Vector3 desiredPosition = mPlayerTransform.position + direction;
            //    mCameraTransform.position = new Vector3(
            //        desiredPosition.x,
            //        rayOrigin.y, // Lock the height to the desired value
            //        desiredPosition.z
            //    );
            //}
            ////------------------------------------------------------------------------------

            //- REFRACTORED CODE ----------------------------------------------------------
            Vector3 rayOrigin = mPlayerTransform.position + Vector3.up * 2.0f; // refractored
            Vector3 direction = mCameraTransform.position - rayOrigin;
            cameraAdjustment(rayOrigin, direction);

            //------------------------------------------------------------------------------
        }

        void cameraAdjustment(Vector3 rayOrigin, Vector3 direction)
        {
            if (Physics.Raycast(rayOrigin, direction.normalized, out RaycastHit hit, direction.magnitude, LayerMask.GetMask("Wall")))
            {
                Vector3 collisionPoint = hit.point - direction.normalized * 0.1f;
                mCameraTransform.position = new Vector3(collisionPoint.x, Mathf.Max(collisionPoint.y, rayOrigin.y), collisionPoint.z);
            }
            else
            {
                Vector3 desiredPosition = mPlayerTransform.position + direction;
                mCameraTransform.position = new Vector3(desiredPosition.x, rayOrigin.y, desiredPosition.z);
            }
        }

        public abstract void Update();
    }
}
