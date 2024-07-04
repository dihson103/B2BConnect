'use server'

import http from '@/lib/http'
import { IndustryResponse } from '@/types/industry.types'
import { ApiSuccessResponse } from '@/types/util.types'

export const getIndustriesAction = (searchTerm: string) => {
  const response = http.get<ApiSuccessResponse<IndustryResponse[] | null>>(
    `/industries?searchTerm=${searchTerm ? searchTerm : ''}`
  )
  return response
}
