type ApiResponse<TError, TData> = {
  status: number
  message: string
  error: TError
  data: TData
}

export type ApiSuccessResponse<TData> = ApiResponse<null, TData>

export type ApiErrorResponse = ApiResponse<any, null>
