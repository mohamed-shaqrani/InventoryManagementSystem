namespace InventoryManagementSystem.App.Response;

public enum ErrorCode
{
    None = 00,
    ValidationError = 01,
    DataBaseError = 02,


    // AUTH
    ChangePasswordError = 100,
    IncorrectPassword = 101,

    // USER
    UserNotFound = 200,
    UserNameExist = 201,
    UserEmailExist = 202,
    UserPhoneExist = 203,
    InvalidCode = 204,
    // Product
    ProductExists = 300,
    ProductDoesNotExist = 301,
    //stock
    InsufficientStock = 400,


}
