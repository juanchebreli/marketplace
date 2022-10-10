using marketplace.Helpers.Enums;
using marketplace.Helpers.Exceptions.Implements;
using marketplace.Helpers.States;

namespace marketplace.Helpers.Factory
{
    public static class StateFactory
    {
        public static State GetState(StatesEnum state)
        {
            switch (state)
            {
                case StatesEnum.FREE:
                    return new Free();
                case StatesEnum.RESERVED:
                   return new Reserved();
                case StatesEnum.SOLDOUT:
                    return new SoldOut();
                default:
                    throw new NotFoundException("State not found");
            }
        }
    }
}
