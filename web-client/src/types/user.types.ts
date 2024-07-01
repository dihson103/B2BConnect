export type SearchUserOption = {
  searchTearm: string | null
  roleId: number | null
  isActive: 'true' | 'false'
  pageIndex: number
  pageSize: number
}

export type User = {
  id: string
  firstName: string
  lastName: string
  phone: string
  address: string
  gender: string
  dob: string
  salaryByDay: number
  isActive: boolean
  roleId: number
}

export type LoginType = {
  id: string
  password: string
}
