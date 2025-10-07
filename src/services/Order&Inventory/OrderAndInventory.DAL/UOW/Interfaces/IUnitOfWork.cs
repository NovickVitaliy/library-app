using OrderAndInventory.DAL.Repositories.Interfaces;

namespace OrderAndInventory.DAL.UOW.Interfaces;

public interface IUnitOfWork
{
    IInventoryRepository InventoryRepository { get; init; }
    IMemberRepository MemberRepository { get; init; }
    IOrderItemRepository OrderItemRepository { get; init; }
    IOrderRepository OrderRepository { get; init; }
    IPaymentRepository PaymentRepository { get; init; }
    IStaffRepository StaffRepository { get; init; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}