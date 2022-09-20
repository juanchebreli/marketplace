using AutoMapper;
using marketplace.DTO.PaymentMethodDTO.CardMethodDTO;
using marketplace.DTO.PaymentMethodDTO.CashMethodDTO;
using marketplace.Models;

namespace marketplace.Services.Interfaces
{
    public interface IPaymentService
    {
        List<PaymentMethod> GetAll();
        List<CashMethod> GetAllCash();
        List<CardMethod> GetAllCard();
        PaymentMethod Get(int id);
        CardMethod Add(CardMethod entity);
        CashMethod Add(CashMethod entity);
        CardMethod Update(CardMethodUpdateDTO entity);
        CashMethod Update(CashMethodUpdateDTO entity);
        void Delete(int id);
    }
}
