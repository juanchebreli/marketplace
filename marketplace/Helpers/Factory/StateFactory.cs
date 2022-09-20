using marketplace.Helpers.Enums;
using marketplace.Helpers.Exceptions.Implements;
using marketplace.Helpers.States;

namespace marketplace.Helpers.Factory
{
    public static class StateFactory
    {
        public static State GetState(int state)
        {
            switch (state)
            {
                case (int)StatesEnum.FREE:
                    return new Free();
                case (int)StatesEnum.RESERVED:
                   return new Reserved();
                case (int)StatesEnum.SOLDOUT:
                    return new SoldOut();
                default:
                    throw new NotFoundException("State not found");
            }
        }
    }
}
