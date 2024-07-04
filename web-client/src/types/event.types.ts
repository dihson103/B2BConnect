import { SearchResponse } from '@/types/util.types'

export type Event = {
  id: number
  name: string
  startDate: string
  endDate: string
  numberCompany: string
  status: number
  statusDescription: string
  location: string
  image: string
  description: string
}

export type SearchEvent = SearchResponse<Event[] | null>

export type SearchEventOption = {
  searchTearm: string | null
  isActive: number
  pageIndex: number
  pageSize: number
}
