using System.Threading.Tasks;
using Bit8.Students.Domain.Models;
using Bit8.Students.Persistence;
using Bit8.Students.Persistence.Repositories;
using Bit8.Students.Services.Students;
using Moq;
using NUnit.Framework;

namespace Bit8.Students.Services.Tests
{
    public class StudentServiceTests
    {
        private Mock<IUnitOfWork> _uowMock;
        private Mock<IStudentRepository> _studentRepositoryMock;
        private Mock<ISemesterRepository> _semesterRepositoryMock;

        private IStudentService _sut;
        
        [SetUp]
        public void Setup()
        {
            _studentRepositoryMock = new Mock<IStudentRepository>();
            _semesterRepositoryMock = new Mock<ISemesterRepository>();
            
            _uowMock = new Mock<IUnitOfWork>();
            _uowMock.SetupGet(x => x.StudentRepository).Returns(_studentRepositoryMock.Object);
            _uowMock.SetupGet(x => x.SemesterRepository).Returns(_semesterRepositoryMock.Object);
            
            _sut = new StudentService(_uowMock.Object);
        }

        [Test]
        public void CreateAsync_ValidData_ShouldSuccess()
        {
            Student studentCallback = null;

            _studentRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Student>()))
                .Callback<Student>(c => studentCallback = c);
            
            // given
            var request = new CreateStudentRequest { Name = "Example name" };
            
            // when
            var result = _sut.CreateAsync(request);

            // then
            Assert.True(result.IsCompletedSuccessfully);
            _studentRepositoryMock.Verify(m => m.AddAsync(It.IsAny<Student>()), Times.Once);
            Assert.AreEqual("Example name", studentCallback.Name);
            _uowMock.Verify(m => m.Commit(), Times.Once);
        }
        
        [Test]
        public async Task AssignToSemesterAsync_ValidData_ShouldSuccess()
        {
            int semesterExistsCallback = 0;
            _semesterRepositoryMock
                .Setup(x => x.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync((int r) => true)
                .Callback<int>(c => semesterExistsCallback = c);
            
            int studentExistsCallback = 0;
            Student updatedStudentCallback = null;
            _studentRepositoryMock
                .Setup(x => x.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync((int r) => true)
                .Callback<int>(c => studentExistsCallback = c);
            _studentRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((int r) => new Student {Id = 20, Name = "Name", SemesterId = 5});
            _studentRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Student>()))
                .Callback<Student>(c => updatedStudentCallback = c);

            // given
            var request = new AssignToSemesterRequest {SemesterId = 10, StudentId = 20};
            
            // when
            var result = await _sut.AssignToSemesterAsync(request);
            
            // then
            Assert.True(result.IsSuccess);
            Assert.AreEqual(10, semesterExistsCallback);
            Assert.AreEqual(20, studentExistsCallback);
            Assert.AreEqual(10, updatedStudentCallback.SemesterId);
            _studentRepositoryMock.Verify(m => m.GetAsync(It.IsAny<int>()), Times.Once);
            _studentRepositoryMock.Verify(m => m.UpdateAsync(It.IsAny<Student>()), Times.Once);
            _uowMock.Verify(m => m.Commit(), Times.Once);
        }
        
        [Test]
        public async Task AssignToSemesterAsync_InvalidData_NonExistentStudentAndSemester_ShouldFail()
        {
            _semesterRepositoryMock
                .Setup(x => x.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync((int r) => false);

            _studentRepositoryMock
                .Setup(x => x.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync((int r) => false);
            // given
            var request = new AssignToSemesterRequest {SemesterId = 10, StudentId = 20};
            
            // when
            var result = await _sut.AssignToSemesterAsync(request);
            
            // then
            Assert.True(result.IsFailed);
            Assert.AreEqual(2, result.Errors.Count);
            _studentRepositoryMock.Verify(m => m.GetAsync(It.IsAny<int>()), Times.Never);
            _studentRepositoryMock.Verify(m => m.UpdateAsync(It.IsAny<Student>()), Times.Never);
            _uowMock.Verify(m => m.Commit(), Times.Never);
        }
        
        [Test]
        public async Task AssignToSemesterAsync_InvalidData_NonExistentStudent_ShouldFail()
        {
            _semesterRepositoryMock
                .Setup(x => x.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync((int r) => true);

            _studentRepositoryMock
                .Setup(x => x.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync((int r) => false);
            // given
            var request = new AssignToSemesterRequest {SemesterId = 10, StudentId = 20};
            
            // when
            var result = await _sut.AssignToSemesterAsync(request);
            
            // then
            Assert.True(result.IsFailed);
            Assert.AreEqual(1, result.Errors.Count);
            _studentRepositoryMock.Verify(m => m.GetAsync(It.IsAny<int>()), Times.Never);
            _studentRepositoryMock.Verify(m => m.UpdateAsync(It.IsAny<Student>()), Times.Never);
            _uowMock.Verify(m => m.Commit(), Times.Never);
        }
        
        [Test]
        public async Task AssignToSemesterAsync_InvalidData_NonExistentSemester_ShouldFail()
        {
            _semesterRepositoryMock
                .Setup(x => x.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync((int r) => false);

            _studentRepositoryMock
                .Setup(x => x.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync((int r) => true);
            // given
            var request = new AssignToSemesterRequest {SemesterId = 10, StudentId = 20};
            
            // when
            var result = await _sut.AssignToSemesterAsync(request);
            
            // then
            Assert.True(result.IsFailed);
            Assert.AreEqual(1, result.Errors.Count);
            _studentRepositoryMock.Verify(m => m.GetAsync(It.IsAny<int>()), Times.Never);
            _studentRepositoryMock.Verify(m => m.UpdateAsync(It.IsAny<Student>()), Times.Never);
            _uowMock.Verify(m => m.Commit(), Times.Never);
        }
    }
}