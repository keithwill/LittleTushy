namespace LittleTushy.Client
{
    public enum StatusCode
    {
        Unknown = 0,
        Ok = 200,
        Created = 201,
        Accepted = 202,
        NoContent = 204,
        BadRequest = 400,
        NotFound = 404,
        Conflict = 409,
        CouldNotProcess = 422,
        ActionNotFound = 454,
        InternalServerError = 500
    }
}