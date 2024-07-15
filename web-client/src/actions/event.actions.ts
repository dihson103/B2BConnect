'use server'

import http from '@/lib/http'
import { CreateEventType, Event, EventDetail, SearchEventOption, UpdateEventType } from '@/types/event.types'
import { ApiSuccessResponse, SearchResponse } from '@/types/util.types'
import { revalidatePath } from 'next/cache'

export const createEventAction = (body: CreateEventType) => {
  const response = http.post<ApiSuccessResponse<null>>('/events', body)
  revalidatePath('/admin/events')
  return response
}

export const searchEventAction = (searchParams: SearchEventOption) => {
  const response = http.get<SearchResponse<Event[] | null>>(
    `/events?searchTerm=${searchParams.searchTearm ?? ''}&status=${searchParams.status ?? 0}&pageIndex=${searchParams.pageIndex ?? 1}&pageSize=${searchParams.pageSize ?? 10}`
  )
  return response
}

export const updateEventAction = (id: string, body: UpdateEventType) => {
  const response = http.put<ApiSuccessResponse<null>>(`/events/${id}`, body)
  revalidatePath('/admin/events')
  return response
}

export const getEventById = (id: string) => {
  const response = http.get<ApiSuccessResponse<EventDetail>>(`/events/${id}`)
  revalidatePath(`/admin/events/update/${id}`)
  return response
}
