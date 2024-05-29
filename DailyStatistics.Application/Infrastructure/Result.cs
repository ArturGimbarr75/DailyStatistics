namespace DailyStatistics.Application.Infrastructure;

public class Result<T, TError> where TError : struct, Enum
{
	public T? Value { get; set; }
	public TError? Error { get; set; }

	public static implicit operator bool(Result<T, TError> rhs)
	{
		return rhs.Error is null;
	}

	public static implicit operator Result<T, TError>(T res) => new Result<T, TError>
	{
		Value = res
	};

	public static implicit operator Result<T, TError>(TError error) => new Result<T, TError>
	{
		Error = error
	};

	public static Result<T, TError> Ok(T val) => val;
	public static Result<T, TError> WithError(TError error) => error;
}

public class Result<TError> where TError : struct, Enum
{
	public TError? Error { get; set; }

	public static implicit operator bool(Result<TError> rhs)
	{
		return rhs.Error is null;
	}

	public static implicit operator Result<TError>(TError error) => new Result<TError>
	{
		Error = error
	};
}

public static class Result
{
	public static Result<TError> Ok<TError>() where TError : struct, Enum
	{
		return new Result<TError>();
	}
}

public class InfoResult<T, TError> : Result<T, TError> where TError : struct, Enum
{
	public string[] Info { get; set; } = default!;

	public static implicit operator InfoResult<T, TError>(T res)
	{
		return new InfoResult<T, TError>
		{
			Value = res
		};
	}

	public static implicit operator InfoResult<T, TError>(TError error)
	{
		return new InfoResult<T, TError>
		{
			Error = error
		};
	}

	public static InfoResult<T, TError> WithInfo(TError error, params string[] info)
	{
		return new InfoResult<T, TError>
		{
			Error = error,
			Info = info
		};
	}
}
