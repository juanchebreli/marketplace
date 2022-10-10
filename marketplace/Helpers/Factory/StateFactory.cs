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
                    return State.FREE;

                case StatesEnum.RESERVED:
                   return State.RESERVED;

                case StatesEnum.SOLDOUT:
                    return State.SOLDOUT;

                default:
                    throw new NotFoundException("State not found");
            }
        }
    }
}
