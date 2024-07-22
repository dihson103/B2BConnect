'use server'

import action from '@/actions/common.action'
import http from '@/lib/http'
import { LoginResponse } from '@/types/auth.types'
import { CreateEventType, Event, EventDetail, SearchEventOption, UpdateEventType } from '@/types/event.types'
import { ApiSuccessResponse, SearchResponse } from '@/types/util.types'
import { revalidatePath } from 'next/cache'
import { cookies, headers } from 'next/headers'

export const createEventAction = (body: CreateEventType) => {
  const response = action.post<ApiSuccessResponse<null>>('/events', body)
  revalidatePath('/admin/events')
  return response
}

export const searchEventAction = async (searchParams: SearchEventOption) => {
  const response = await action.get<SearchResponse<Event[] | null>>(
    `/events?searchTerm=${searchParams.searchTerm ?? ''}&status=${searchParams.status ?? 0}&pageIndex=${searchParams.pageIndex ?? 1}&pageSize=${searchParams.pageSize ?? 10}`
  )
  return response
}

export const updateEventAction = (id: string, body: UpdateEventType) => {
  const response = action.put<ApiSuccessResponse<null>>(`/events/${id}`, body)
  revalidatePath('/admin/events')
  revalidatePath(`/admin/events/update/${id}`)
  revalidatePath(`/admin/events/${id}`)
  return response
}

export const getEventById = async (id: string) => {
  const loginResponse = cookies().get('loginResponse')?.value
  if (!loginResponse) throw new Error('Không có quyền xem')

  try {
    const token: LoginResponse = JSON.parse(loginResponse)
    const response = await http.get<ApiSuccessResponse<EventDetail>>(`/events/${id}`, {
      headers: {
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + token.accessToken
      }
    })

    return response
  } catch (error: any) {
    throw new Error('Không có quyền xem')
  }
}
