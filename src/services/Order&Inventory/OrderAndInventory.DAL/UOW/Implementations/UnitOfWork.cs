using OrderAndInventory.DAL.Database;
using OrderAndInventory.DAL.Repositories.Interfaces;
using OrderAndInventory.DAL.UOW.Interfaces;

namespace OrderAndInventory.DAL.UOW.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly OrderAndInventoryDbContext _db;
    
    public UnitOfWork(OrderAndInventoryDbContext db, 
        IInventoryRepository inventoryRepository, 
        IMemberRepository memberRepository, 
        IOrderItemRepository orderItemRepository, 
        IOrderRepository orderRepository, 
        IPaymentRepository paymentRepository, 
        IStaffRepository staffRepository)
    {
        _db = db;
        InventoryRepository = inventoryRepository;
        MemberRepository = memberRepository;
        OrderItemRepository = orderItemRepository;
        OrderRepository = orderRepository;
        PaymentRepository = paymentRepository;
        StaffRepository = staffRepository;
    }
    
    public IInventoryRepository InventoryRepository { get; init; }
    public IMemberRepository MemberRepository { get; init; }
    public IOrderItemRepository OrderItemRepository { get; init; }
    public IOrderRepository OrderRepository { get; init; }
    public IPaymentRepository PaymentRepository { get; init; }
    public IStaffRepository StaffRepository { get; init; }
    
    public int SaveChanges()
    {
        return _db.SaveChanges();
    }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _db.SaveChangesAsync(cancellationToken);
    }
}