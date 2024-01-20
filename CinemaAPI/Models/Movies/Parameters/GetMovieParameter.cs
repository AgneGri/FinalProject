﻿using CinemaApi.Exceptions;

namespace CinemaApi.Models.Movies.Parameters
{
	public class GetMovieParameter
	{
		public GetMovieParameter(int id)
		{
			if (id < 1)
			{
				throw new DataValidationException("Invalid movie Id.");
			}

			Id = id;
		}

		public int Id { get; }
	}
}