namespace LittleTushy.Client
{
    /// <summary>
    /// Standard status codes returned from a controller action call.
    /// These have be broadly pulled from HTTP status codes for familiarity.
    /// </summary>
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
        //I made this one up for Little Tushy
        ActionNotFound = 454,
        InternalServerError = 500
    }
}