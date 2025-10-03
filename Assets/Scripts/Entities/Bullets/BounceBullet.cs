namespace Game.Enitites.Bullets
{
    public class BounceBullet : Bullet
    {
        private const float Bounce_Radius = 8f;

        private int _bounces = 3;

        protected override void OnHit()
        {
            DoBounce();
        }

        protected override void OnDespawn()
        {
            if (_bounces <= 0 || target == null)
            {
                base.OnDespawn();
            }
        }

        private void DoBounce()
        {
            if (_bounces > 0)
            {
                base.OnHit();
                _bounces--;
                IHittable newTarget = GetRandomHittable(FindHittablesInRange(transform.position, Bounce_Radius), target);
                SetTarget(newTarget);
            }
            else
            {
                OnDespawn();
            }
        }
    }
}