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
  description: string
  industryIds: string[]
  images: ImageRequest[]
}

export type ImageRequest = {
  image: string
  isMain: boolean
}

export type UpdateEventType = {
  name: string
  startAt: string
  endAt: string
  location: string
  description: string
  industryIds: string[]
  imageRequests: ImageRequest[]
  status: number
}

export type ImageResponse = {
  id: string
  path: string
  isMain: boolean
}

export type EventDetail = {
  id: string
  name: string
  startAt: string
  endAt: string
  location: string
  description: string
  status: number
  statusDescription: string
  industries: IndustryResponse[]
  images: ImageResponse[]
}
