'use server'

import http from '@/lib/http'
import { CreateEventType, Event, SearchEventOption } from '@/types/event.types'
import { ApiSuccessResponse, SearchResponse } from '@/types/util.types'
import { revalidatePath } from 'next/cache'

export const createEventAction = (body: CreateEventType) => {
  const response = http.post<ApiSuccessResponse<null>>('/events', body)
  revalidatePath('/admin/events')
  return response
}

export const searchEventAction = (searchParams: SearchEventOption) => {
  const response = http.get<SearchResponse<Event[] | null>>(
    `/events?searchTerm=${searchParams.searchTearm ?? ''}&status=${0}&pageIndex=${searchParams.pageIndex ?? 1}&pageSize=${searchParams.pageSize ?? 10}`
  )
  return response
}
