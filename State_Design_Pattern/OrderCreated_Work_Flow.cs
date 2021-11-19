using System;

namespace State_Design_Pattern
{
    public enum OrderActionEnum
    {
        ORDER,
        STOCK,
        TASK,
        ROBOT,
        ALLOCATION
    }

    public enum OrderStatusEnum
    {
        START,
        STOP,
        RUNNING
    }

    public class OrderCreated_Work_Flow
    {
        public OrderCreated_Work_Flow()
        {
            var externalState = OrderStatusEnum.STOP;

            var carStateless = new Stateless.StateMachine<OrderStatusEnum, OrderActionEnum>(
                () => externalState,
                s => externalState = s
                );

            carStateless.Configure(OrderStatusEnum.STOP)
                .Permit(OrderActionEnum.ORDER, OrderStatusEnum.START);

            carStateless.Configure(OrderStatusEnum.START)
               .Permit(OrderActionEnum.STOCK, OrderStatusEnum.RUNNING)
               .OnEntry(a => Console.WriteLine("STOCK : "+a.Source + " - " + a.Destination));

            carStateless.Configure(OrderStatusEnum.RUNNING)
                .Permit(OrderActionEnum.ALLOCATION, OrderStatusEnum.STOP)
                .Ignore(OrderActionEnum.TASK)
                .Ignore(OrderActionEnum.ROBOT);

            Console.WriteLine($"State: {externalState}");

            carStateless.Fire(OrderActionEnum.ORDER);
            Console.WriteLine($"State: {externalState}");

            carStateless.Fire(OrderActionEnum.STOCK);
            Console.WriteLine($"State: {externalState}");

            //carStateless.Fire(OrderActionEnum.TASK);
            //Console.WriteLine($"State: {externalState}");

            carStateless.Fire(OrderActionEnum.ROBOT);
            Console.WriteLine($"State: {externalState}");

            carStateless.Fire(OrderActionEnum.ALLOCATION);
            Console.WriteLine($"State: {externalState}");
        }
    }
}