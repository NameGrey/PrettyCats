using System;
using BookIt.BLL.Entities;

namespace BookIt.BLL.Services
{
    public interface IBookingService
    {
        bool Book(BookingOfferDto bookingOfferDto, int slotId, DateTime startDate, DateTime endDate, UserDto person);

        /// <summary>
        /// ����������� ���������� ����������� �� ��������� ������
        /// </summary>
        /// <param name="startDate">���� ������ ��������������</param>
        /// <param name="endDate">���� ��������� ��������������</param>
        /// <returns>�������, ������� ������������� ��� ���</returns>
        bool Book(BookingOfferDto bookingOfferDto, DateTime startDate, DateTime endDate, UserDto person);

        bool UnBook(BookingOfferDto bookingOfferDto, int slotId, UserDto person);

        /// <summary>
        /// ��������� ���������� ����������� ��� ������������ ������� � �������� ���������� � �������
        /// </summary>
        /// <param name="subject"></param>
        void FillFromSubject(BookingOfferDto bookingOfferDto, BookingSubjectDto subject);

        void FillCustomBookingOffer(BookingOfferDto bookingOfferDto);
    }
}