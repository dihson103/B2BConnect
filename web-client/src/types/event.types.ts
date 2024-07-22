import { IndustryResponse } from '@/types/industry.types'
import { SearchResponse } from '@/types/util.types'

export type Event = {
  id: string
  name: string
  startAt: string
  endAt: string
  status: number
  statusDescription: string
  location: string
  image: string
  description: string
}

export type SearchEvent = SearchResponse<Event[] | null>

export type SearchEventOption = {
  searchTerm: string | null
  status: number
  pageIndex: number
  pageSize: number
}

export type CreateEventType = {
  name: string
  startAt: string
  endAt: string
  location: string
  image: string
  description: string
  industryIds: string[]
}

export type UpdateEventType = CreateEventType & {
  status: number
}

export type EventDetail = Event & {
  industries: IndustryResponse[]
}
