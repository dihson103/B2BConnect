import { BranchResponse } from '@/types/branch.type'
import { RepresentativeResponse } from '@/types/representative.types'
import { SearchResponse } from '@/types/util.types'

export type BusinessResponse = {
  id: string
  taxCode: string
  name: string
  dateOfEstablishment: string
  webSite: string
  avatarImage: string
  coverImage: string
  numberOfEmployee: number
  isVerified: boolean
  representative?: RepresentativeResponse | null
  branches?: BranchResponse[] | null
}

export type SearchBusinessOption = {
  searchTearm: string | null
  isVerified: boolean
  pageIndex: number
  pageSize: number
}

export type SearchBusinessResponse = SearchResponse<BusinessResponse[]>
