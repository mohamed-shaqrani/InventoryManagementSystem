namespace InventoryManagementSystem.App.Response;

public enum SuccessCode
{
    None = 00,
    OperationCompleted = 01,
    OperationSucceeded = 02,
    // AUTH
    ChangePasswordUpdated = 100,
    LoginCorrectly = 101,

    // USER
    UserCreated = 201,
    UserDeleted = 202,
    UsersRetrieved = 203,
    UserDetailsRetrieved = 204,

}