import http from '@/lib/http'
import { Users } from '@/types/user.types'

export const getUsers = () => {
  const response = http.get<Users>('/users')
  return response
}

export const createUser = (body: any) => {
  const response = http.post<any>('/user', body)
  return response
}
