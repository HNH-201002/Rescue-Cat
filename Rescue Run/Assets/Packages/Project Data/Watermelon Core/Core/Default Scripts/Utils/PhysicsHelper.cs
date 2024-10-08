using UnityEngine;

namespace Watermelon
{
    public static class PhysicsHelper
    {
        public static readonly int LAYER_LAND = LayerMask.NameToLayer("Land");
        public static readonly int LAYER_DEFAULT = LayerMask.NameToLayer("Default");

        public const string TAG_PLAYER = "Player";
        public const string TAG_NURSE = "Nurse";
        public const string TAG_VISITOR = "Visitor";
        public const string TAG_ANIMAL = "Animal";
        public const string TAG_COLLECTED = "Collected";
        public const string TAG_CHARACTER = "Character";

        public static void Init()
        {

        }
    }
}