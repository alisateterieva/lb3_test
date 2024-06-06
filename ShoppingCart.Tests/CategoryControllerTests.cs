//using Moq;
//using ShoppingCart.DataAccess.Repositories;
//using ShoppingCart.DataAccess.ViewModels;
//using ShoppingCart.Models;
//using ShoppingCart.Tests.Datasets;
//using System.Collections.Generic;
//using ShoppingCart.Web.Areas.Admin.Controllers;
//using System.Linq.Expressions;
//using Xunit;

//namespace ShoppingCart.Tests
//{
//    public class CategoryControllerTests
//    {
//        [Fact]
//        public void GetCategories_All_ReturnAllCategories()
//        {
//            // Arrange
//            Mock<ICategoryRepository> repositoryMock = new Mock<ICategoryRepository>();

//            repositoryMock.Setup(r => r.GetAll(It.IsAny<string>()))
//                .Returns(() => CategoryDataset.Categories);
//            var mockUnitOfWork = new Mock<IUnitOfWork>();
//            mockUnitOfWork.Setup(uow => uow.Category).Returns(repositoryMock.Object);
//            var controller = new CategoryController(mockUnitOfWork.Object);

//            // Act
//            var result = controller.Get();

//            // Assert
//            Assert.Equal(CategoryDataset.Categories, result.Categories);
//        }
//        // Перевірка функції отримання конкретної категорії
//        [Fact]
//        public void GetCategory_ById_ReturnCategory()
//        {
//            // Arrange
//            var category = new Category { Id = 1, Name = "Test Category" };
//            Mock<ICategoryRepository> repositoryMock = new Mock<ICategoryRepository>();

//            repositoryMock.Setup(r => r.GetT(It.IsAny<Expression<Func<Category, bool>>>()))
//                .Returns(category);
//            var mockUnitOfWork = new Mock<IUnitOfWork>();
//            mockUnitOfWork.Setup(uow => uow.Category).Returns(repositoryMock.Object);
//            var controller = new CategoryController(mockUnitOfWork.Object);

//            // Act
//            var result = controller.Get(1);

//            // Assert
//            Assert.Equal(category, result.Category);
//        }
//        // Перевірка функції додавання категорії
//        [Fact]
//        public void CreateUpdate_ValidCategory_AddsCategory()
//        {
//            // Arrange
//            var categoryVM = new CategoryVM { Category = new Category { Id = 0, Name = "New Category" } };
//            Mock<ICategoryRepository> repositoryMock = new Mock<ICategoryRepository>();

//            repositoryMock.Setup(r => r.Add(It.IsAny<Category>()));
//            var mockUnitOfWork = new Mock<IUnitOfWork>();
//            mockUnitOfWork.Setup(uow => uow.Category).Returns(repositoryMock.Object);

//            var controller = new CategoryController(mockUnitOfWork.Object);

//            // Act
//            controller.CreateUpdate(categoryVM);

//            // Assert
//            repositoryMock.Verify(r => r.Add(It.IsAny<Category>()), Times.Once);
//            mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
//        }
//        // Перевірка функції оновлення категорії
//        [Fact]
//        public void CreateUpdate_ExistingCategory_UpdatesCategory()
//        {
//            // Arrange
//            var categoryVM = new CategoryVM { Category = new Category { Id = 1, Name = "Updated Category" } };
//            Mock<ICategoryRepository> repositoryMock = new Mock<ICategoryRepository>();

//            repositoryMock.Setup(r => r.Update(It.IsAny<Category>()));
//            var mockUnitOfWork = new Mock<IUnitOfWork>();
//            mockUnitOfWork.Setup(uow => uow.Category).Returns(repositoryMock.Object);

//            var controller = new CategoryController(mockUnitOfWork.Object);

//            // Act
//            controller.CreateUpdate(categoryVM);

//            // Assert
//            repositoryMock.Verify(r => r.Update(It.IsAny<Category>()), Times.Once);
//            mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
//        }
//        // Перевірка функції видалення конкретної категорії - правильний айді
//        [Fact]
//        public void DeleteData_ValidId_DeletesCategory()
//        {
//            // Arrange
//            var category = new Category { Id = 1, Name = "Test Category" };
//            Mock<ICategoryRepository> repositoryMock = new Mock<ICategoryRepository>();

//            repositoryMock.Setup(r => r.GetT(It.IsAny<Expression<Func<Category, bool>>>()))
//                .Returns(category);
//            repositoryMock.Setup(r => r.Delete(It.IsAny<Category>()));
//            var mockUnitOfWork = new Mock<IUnitOfWork>();
//            mockUnitOfWork.Setup(uow => uow.Category).Returns(repositoryMock.Object);

//            var controller = new CategoryController(mockUnitOfWork.Object);

//            // Act
//            controller.DeleteData(1);

//            // Assert
//            repositoryMock.Verify(r => r.Delete(It.IsAny<Category>()), Times.Once);
//            mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
//        }
//        // Перевірка функції видалення конкретної категорії - неправильний айді
//        [Fact]
//        public void DeleteData_InvalidId_ThrowsException()
//        {
//            // Arrange
//            Mock<ICategoryRepository> repositoryMock = new Mock<ICategoryRepository>();

//            repositoryMock.Setup(r => r.GetT(It.IsAny<Expression<Func<Category, bool>>>()))
//                .Returns((Category)null);
//            var mockUnitOfWork = new Mock<IUnitOfWork>();
//            mockUnitOfWork.Setup(uow => uow.Category).Returns(repositoryMock.Object);

//            var controller = new CategoryController(mockUnitOfWork.Object);

//            // Act & Assert
//            var exception = Assert.Throws<Exception>(() => controller.DeleteData(1));
//            Assert.Equal("Category not found", exception.Message);
//        }
//    }
//}
