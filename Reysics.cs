using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace reysics
{
    public static class Reysics
    {
        public static Vector3 Gravity = -Vector3.up * 14f;
        public static List<ReysicsObject> Objects = new List<ReysicsObject>();
        
        public static bool WithoutBoxCast(Vector3 point, Vector3 scale, Quaternion rotation, out ReysicsHit hit, ReysicsObject without)
        {
            foreach (ReysicsObject reysicsObject in Objects)
            {
                if (reysicsObject != without)
                {
                    hit = reysicsObject.BoxCast(point, scale, rotation);
                    if (hit != null)
                    {
                        return true;
                    }
                }
            }
            hit = null;
            return false;
        }
        public static bool BoxCast(Vector3 point, Vector3 scale, Quaternion rotation, out ReysicsHit hit)
        {
            foreach (ReysicsObject reysicsObject in Objects)
            {
                hit = reysicsObject.BoxCast(point, scale, rotation);
                if (hit != null)
                {
                    return true;
                }
            }
            hit = null;
            return false;
        }

        public static bool WithoutPointCast(Vector3 point, out ReysicsHit hit, ReysicsObject without)
        {
            foreach (ReysicsObject reysicsObject in Objects)
            {
                if(reysicsObject != without)
                {
                    hit = reysicsObject.PointCast(point);
                    if (hit != null)
                    {
                        return true;
                    }
                }
            }
            hit = null;
            return false;
        }
        public static bool PointCast(Vector3 point, out ReysicsHit hit)
        {
            foreach(ReysicsObject reysicsObject in Objects)
            {
                hit = reysicsObject.PointCast(point);
                if (hit != null)
                {
                    return true;
                }
            }
            hit = null;
            return false;
        }
    }

    public class ReysicsObject
    {
        public Vector3 position = Vector3.zero;
        public Quaternion rotation = Quaternion.Euler(Vector3.zero);
        public Vector3 scale = Vector3.one;
        public virtual ReysicsHit PointCast(Vector3 point)
        {
            return null;
        }
        public virtual ReysicsHit BoxCast(Vector3 point, Vector3 scale, Quaternion rotation)
        {
            return null;
        }
        public ReysicsObject()
        {
            Reysics.Objects.Add(this);
        }
    }

    //public class ReysicsRigidBody
    //{
    //    public ReysicsObject Target;
    //    public Vector3 Velocity = Vector3.zero;
    //    public void UpdateTarget()
    //    {
    //        Velocity += Reysics.Gravity;
    //        Velocity += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * 10;

    //        ReysicsHit hitX;
    //        bool MoveX = !Reysics.WithoutBoxCast(Target.position + new Vector3(Velocity.x * Time.deltaTime, 0, 0), Target.scale, Target.rotation, out hitX, Target);
    //        ReysicsHit hitY;
    //        bool MoveY = !Reysics.WithoutBoxCast(Target.position + new Vector3(0, Velocity.y * Time.deltaTime, 0), Target.scale, Target.rotation, out hitY, Target);
    //        ReysicsHit hitZ;
    //        bool MoveZ = !Reysics.WithoutBoxCast(Target.position + new Vector3(0, 0, Velocity.z * Time.deltaTime), Target.scale, Target.rotation, out hitZ, Target);

    //        Vector3 MoveVelocity = Vector3.zero;
    //        if (MoveX) MoveVelocity += new Vector3(Velocity.x, 0, 0);
    //        if (MoveY) MoveVelocity += new Vector3(0, Velocity.y, 0);
    //        if (MoveZ) MoveVelocity += new Vector3(0, 0, Velocity.z);

    //        Velocity = Vector3.zero;

    //        Target.position += MoveVelocity * Time.deltaTime;
    //    }
    //    public ReysicsRigidBody(ReysicsObject target)
    //    {
    //        Target = target;
    //    }
    //}
    public class ReysicsRigidBody
    {
        public ReysicsObject Target;
        public Vector3 Velocity = Vector3.zero;
        public Vector3 AngularVelocity = Vector3.zero; // Add this for rotation
        public float GravityScale = 1.0f;
        
        public virtual void UpdateTarget()
        {
            Velocity += Reysics.Gravity * GravityScale;

            ReysicsHit hitX;
            bool MoveX = !Reysics.WithoutBoxCast(Target.position + new Vector3(Velocity.x * Time.deltaTime, 0, 0), Target.scale, Target.rotation, out hitX, Target);
            ReysicsHit hitY;
            bool MoveY = !Reysics.WithoutBoxCast(Target.position + new Vector3(0, Velocity.y * Time.deltaTime, 0), Target.scale, Target.rotation, out hitY, Target);
            ReysicsHit hitZ;
            bool MoveZ = !Reysics.WithoutBoxCast(Target.position + new Vector3(0, 0, Velocity.z * Time.deltaTime), Target.scale, Target.rotation, out hitZ, Target);

            Vector3 MoveVelocity = Vector3.zero;
            if (MoveX) MoveVelocity += new Vector3(Velocity.x, 0, 0);
            if (MoveY) MoveVelocity += new Vector3(0, Velocity.y, 0);
            if (MoveZ) MoveVelocity += new Vector3(0, 0, Velocity.z);

            Velocity = Vector3.zero;
            ReysicsHit hit;
            if (Reysics.WithoutBoxCast(Target.position, Target.scale, Target.rotation, out hit, Target))
            {
                foreach (Vector3 Point in hit.Point)
                {
                    MoveVelocity += (Target.position - Point) * 1 + Vector3.one * 0.00001f;
                }
            }

            Target.position += MoveVelocity * Time.deltaTime;

            // Add this for rotation
            Quaternion deltaRotation = Quaternion.Euler(AngularVelocity * Time.deltaTime);
            Target.rotation = deltaRotation * Target.rotation;
        }
        public void AddMove(Vector3 move) => Velocity += move;
        public ReysicsRigidBody(ReysicsObject target)
        {
            Target = target;
        }
        public ReysicsRigidBody() { }
    }
    public class ReysicsSlowBody : ReysicsRigidBody
    {
        public Vector3 SlowVelcotiy = Vector3.zero;
        public float Unit = 20;
        public override void UpdateTarget()
        {
            Velocity += SlowVelcotiy * Time.deltaTime * Unit;
            SlowVelcotiy -= SlowVelcotiy * Time.deltaTime * Unit;
            base.UpdateTarget();
        }
        public void AddForce(Vector3 force) => SlowVelcotiy += force;
        public ReysicsSlowBody(ReysicsObject target)
        {
            Target = target;
        }
    }

    public class ReysicsHit
    {
        public Vector3[] Point;
        public ReysicsObject Object;
    }

    namespace shapes
    {
        public class ReysicsBox : ReysicsObject
        {
            public override ReysicsHit PointCast(Vector3 point)
            {
                if (IsPointInBox(point, position, rotation, scale))
                {
                    return new ReysicsHit()
                    {
                        Point = new Vector3[] { point },
                        Object = this
                    };
                }
                else return null;
            }
            public override ReysicsHit BoxCast(Vector3 point, Vector3 scale, Quaternion rotation)
            {
                var ibib = IntersectionPoints(point, rotation, scale, position, this.rotation, this.scale);
                if (ibib.Count != 0)
                {
                    return new ReysicsHit()
                    {
                        Point = ibib.ToArray(),
                        Object = this
                    };
                }
                else return null;
            }

            public static List<Vector3> IntersectionPoints(Vector3 boxAPosition, Quaternion boxARotation, Vector3 boxAScale, Vector3 boxBPosition, Quaternion boxBRotation, Vector3 boxBScale)
            {
                // Create the box's extents (half scale)
                Vector3 boxAExtents = boxAScale / 2.0f;
                Vector3 boxBExtents = boxBScale / 2.0f;

                // Define all eight corners of Box A in its local space
                Vector3[] boxACorners = new Vector3[8]
                {
        new Vector3(-boxAExtents.x, -boxAExtents.y, -boxAExtents.z),
        new Vector3(boxAExtents.x, -boxAExtents.y, -boxAExtents.z),
        new Vector3(-boxAExtents.x, boxAExtents.y, -boxAExtents.z),
        new Vector3(boxAExtents.x, boxAExtents.y, -boxAExtents.z),
        new Vector3(-boxAExtents.x, -boxAExtents.y, boxAExtents.z),
        new Vector3(boxAExtents.x, -boxAExtents.y, boxAExtents.z),
        new Vector3(-boxAExtents.x, boxAExtents.y, boxAExtents.z),
        new Vector3(boxAExtents.x, boxAExtents.y, boxAExtents.z)
                };

                // List to store intersection points
                List<Vector3> intersections = new List<Vector3>();

                // Check each corner of Box A to see if it's inside Box B
                foreach (Vector3 localCorner in boxACorners)
                {
                    // Transform the corner into world space
                    Vector3 worldCorner = boxARotation * localCorner + boxAPosition;

                    // Transform the corner into Box B's local space
                    Vector3 localCornerInB = Quaternion.Inverse(boxBRotation) * (worldCorner - boxBPosition);

                    // Check if the corner is inside Box B
                    if (localCornerInB.x >= -boxBExtents.x && localCornerInB.x <= boxBExtents.x &&
                        localCornerInB.y >= -boxBExtents.y && localCornerInB.y <= boxBExtents.y &&
                        localCornerInB.z >= -boxBExtents.z && localCornerInB.z <= boxBExtents.z)
                    {
                        // If the corner of Box A is inside Box B, add it to the intersections list
                        intersections.Add(worldCorner);
                    }
                }

                // Return the list of intersection points
                return intersections;
            }


            public static bool IsPointInBox(Vector3 point, Vector3 boxPosition, Quaternion boxRotation, Vector3 boxScale)
            {
                // Transform the point into the box's local space
                Vector3 localPoint = Quaternion.Inverse(boxRotation) * (point - boxPosition);

                // Create the box's extents (half scale)
                Vector3 boxExtents = boxScale / 2.0f;

                // Check if the point is inside the box
                return (localPoint.x >= -boxExtents.x && localPoint.x <= boxExtents.x) &&
                       (localPoint.y >= -boxExtents.y && localPoint.y <= boxExtents.y) &&
                       (localPoint.z >= -boxExtents.z && localPoint.z <= boxExtents.z);
            }
        }
        //public class ReysicsSphere : ReysicsObject
        //{
        //    public override ReysicsHit PointCast(Vector3 point)
        //    {
        //        if (IsPointInSphere(point, position, rotation, scale))
        //        {
        //            return new ReysicsHit()
        //            {
        //                Point = point,
        //                Object = this
        //            };
        //        }
        //        else return null;
        //    }
        //    public static bool IsPointInSphere(Vector3 point, Vector3 spherePosition, Quaternion sphereRotation, Vector3 sphereScale)
        //    {
        //        // Transform the point into the sphere's local space
        //        Vector3 localPoint = Quaternion.Inverse(sphereRotation) * (point - spherePosition);

        //        // Scale the point by the inverse of the sphere's scale
        //        Vector3 scaledPoint = new Vector3(localPoint.x / sphereScale.x, localPoint.y / sphereScale.y, localPoint.z / sphereScale.z);

        //        // Check if the point is inside the sphere
        //        return scaledPoint.sqrMagnitude <= 1.0f;
        //    }

        //}
    }
}
