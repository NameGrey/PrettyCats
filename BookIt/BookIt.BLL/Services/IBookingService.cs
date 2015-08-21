using System;
using BookIt.BLL.Entities;

namespace BookIt.BLL.Services
{
    public interface IBookingService
    {
        bool Book(BookingOfferDto bookingOfferDto, int slotId, DateTime startDate, DateTime endDate, UserDto user);

        /// <summary>
        /// Ѕронировать конкретное предложение на указанный период
        /// </summary>
        /// <param name="startDate">ƒата начала резервировани€</param>
        /// <param name="endDate">ƒата окончани€ резервировани€</param>
        /// <returns>ѕризнак, успешно забронировано или нет</returns>
        bool Book(BookingOfferDto bookingOfferDto, DateTime startDate, DateTime endDate, UserDto user);

        bool UnBook(BookingOfferDto bookingOfferDto, int slotId, UserDto person);

        /// <summary>
        /// ќбновл€ет полученное предложение дл€ бронировани€ данными о предмете бронивани€ и времени
        /// </summary>
        /// <param name="subject"></param>
        void FillFromSubject(BookingOfferDto bookingOfferDto, BookingSubjectDto subject);

        void FillCustomBookingOffer(BookingOfferDto bookingOfferDto);
    }
}