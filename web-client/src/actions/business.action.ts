import http from '@/lib/http'
import { SearchBusinessOption, SearchBusinessResponse } from '@/types/business.types'

export const searchBusinessAction = (searchParams: SearchBusinessOption) => {
  const response = http.get<SearchBusinessResponse>(
    `/businesses/byAdmin?searchTerm=${searchParams.searchTearm ?? ''}&isVerified=${searchParams.isVerified ?? true}&pageIndex=${searchParams.pageIndex ?? 1}&pageSize=${searchParams.pageSize ?? 10}`
  )
  return response
}
