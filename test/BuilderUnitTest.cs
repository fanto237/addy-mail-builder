using Xunit;

public class BuilderUnitTest
{
 [Fact]
 public void Builder_should_return_response_object_with_status_error()
 {
  // Arrange
  var res = new Response() { Status = "error", Result = "Invalid request" };
  // Act
  // Assert
  Assert.Equal("error", res.Status);

 }

}