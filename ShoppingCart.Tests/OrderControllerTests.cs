//using Moq;
//using ShoppingCart.DataAccess.Repositories;
//using ShoppingCart.DataAccess.ViewModels;
//using ShoppingCart.Models;
//using ShoppingCart.Tests.Datasets;
//using System.Collections.Generic;
//using ShoppingCart.Web.Areas.Admin.Controllers;
//using System.Linq.Expressions;
//using Xunit;
//using ShoppingCart.Utility;
//using Stripe;

//namespace ShoppingCart.Tests
//{
//    public class CategoryControllerTests
//    {
//        [Fact]
//        public void OrderDetails_ReturnsOrderDetails()
//        {
//            // Arrange
//            var orderId = 1;
//            var orderHeader = new OrderHeader { Id = orderId };
//            var orderDetails = new List<OrderDetail> { new OrderDetail { Id = 1 }, new OrderDetail { Id = 2 } };
//            var unitOfWorkMock = new Mock<IUnitOfWork>();
//            unitOfWorkMock.Setup(uow => uow.OrderHeader.GetT(It.IsAny<Func<OrderHeader, bool>>(), It.IsAny<string>()))
//                .Returns(orderHeader);
//            unitOfWorkMock.Setup(uow => uow.OrderDetail.GetAll(It.IsAny<string>()))
//                .Returns(orderDetails);
//            var controller = new OrderController(unitOfWorkMock.Object);

//            // Act
//            var result = controller.OrderDetails(orderId);

//            // Assert
//            Assert.Equal(orderHeader, result.OrderHeader);
//            Assert.Equal(orderDetails, result.OrderDetails);
//        }

//        [Fact]
//        public void SetToInProcess_UpdatesOrderStatusToInProcess()
//        {
//            // Arrange
//            var orderId = 1;
//            var orderVM = new OrderVM { OrderHeader = new OrderHeader { Id = orderId } };
//            var unitOfWorkMock = new Mock<IUnitOfWork>();
//            var controller = new OrderController(unitOfWorkMock.Object);

//            // Act
//            controller.SetToInProcess(orderVM);

//            // Assert
//            unitOfWorkMock.Verify(uow => uow.OrderHeader.UpdateStatus(orderId, OrderStatus.StatusInProcess), Times.Once);
//            unitOfWorkMock.Verify(uow => uow.Save(), Times.Once);
//        }

//        [Fact]
//        public void SetToShipped_UpdatesOrderStatusToShipped()
//        {
//            // Arrange
//            var orderId = 1;
//            var orderVM = new OrderVM { OrderHeader = new OrderHeader { Id = orderId } };
//            var unitOfWorkMock = new Mock<IUnitOfWork>();
//            var controller = new OrderController(unitOfWorkMock.Object);

//            // Act
//            controller.SetToShipped(orderVM);

//            // Assert
//            unitOfWorkMock.Verify(uow => uow.OrderHeader.Update(It.IsAny<OrderHeader>()), Times.Once);
//            unitOfWorkMock.Verify(uow => uow.Save(), Times.Once);
//        }

//        [Fact]
//        public void SetToCancelOrder_CancelsOrder()
//        {
//            // Arrange
//            var orderId = 1;
//            var orderHeader = new OrderHeader { Id = orderId, PaymentStatus = PaymentStatus.StatusApproved };
//            var orderVM = new OrderVM { OrderHeader = orderHeader };
//            var unitOfWorkMock = new Mock<IUnitOfWork>();
//            unitOfWorkMock.Setup(uow => uow.OrderHeader.GetT(It.IsAny<Func<OrderHeader, bool>>()))
//                .Returns(orderHeader);
//            var controller = new OrderController(unitOfWorkMock.Object);

//            // Act
//            controller.SetToCancelOrder(orderVM);

//            // Assert
//            unitOfWorkMock.Verify(uow => uow.OrderHeader.UpdateStatus(orderId, OrderStatus.StatusCancelled), Times.Once);
//            unitOfWorkMock.Verify(uow => uow.Save(), Times.Once);
//        }

//        [Fact]
//        public void SetToCancelOrder_WithApprovedPayment_CreatesRefund()
//        {
//            // Arrange
//            var orderId = 1;
//            var paymentIntentId = "pi_123456789";
//            var orderHeader = new OrderHeader { Id = orderId, PaymentStatus = PaymentStatus.StatusApproved, PaymentIntentId = paymentIntentId };
//            var orderVM = new OrderVM { OrderHeader = orderHeader };
//            var refundOptions = new RefundCreateOptions
//            {
//                Reason = RefundReasons.RequestedByCustomer,
//                PaymentIntent = paymentIntentId
//            };
//            var refundServiceMock = new Mock<RefundService>();
//            refundServiceMock.Setup(service => service.Create(It.IsAny<RefundCreateOptions>()));
//            var unitOfWorkMock = new Mock<IUnitOfWork>();
//            unitOfWorkMock.Setup(uow => uow.OrderHeader.GetT(It.IsAny<Func<OrderHeader, bool>>()))
//                .Returns(orderHeader);
//            var controller = new OrderController(unitOfWorkMock.Object);

//            // Act
//            controller.SetToCancelOrder(orderVM);

//            // Assert
//            unitOfWorkMock.Verify(uow => uow.OrderHeader.UpdateStatus(orderId, OrderStatus.StatusCancelled), Times.Once);
//            unitOfWorkMock.Verify(uow => uow.Save(), Times.Once);
//            refundServiceMock.Verify(service => service.Create(refundOptions), Times.Once);
//        }

//    }

//}
