'use server'

import http from '@/lib/http'
import { LoginType } from '@/types/user.types'

export const loginAction = (body: LoginType) => {
  return http.post<any>('/login', body, { baseUrl: 'https://capstone-backend.online/api/auth' })
}
