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

export type SearchBusinessQueryOption = {
  searchTerm: string | null
  industries: string | null
  company_size: string | null
  isVerified: string | null
  number_year_operation: string | null
}

export type SearchBusinessQueryOptionKeys = keyof SearchBusinessQueryOption
