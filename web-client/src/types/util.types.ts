type ApiResponse<TError, TData> = {
  status: number
  message: string
  error: TError
  data: TData
}

export type ApiSuccessResponse<TData> = ApiResponse<null, TData>

export type ApiErrorResponse = ApiResponse<any, null>

type SearchResponseType<T> = {
  currentPage: number
  totalPages: number
  data: T
}

export type SearchResponse<T> = ApiSuccessResponse<SearchResponseType<T>>
