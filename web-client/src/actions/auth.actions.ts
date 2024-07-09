'use server'

import http from '@/lib/http'
import { LoginSuccessResponse, LoginType, RegisterSuccessResponse, RegisterType } from '@/types/auth.types'

export const loginAction = (body: LoginType) => {
  return http.post<LoginSuccessResponse>('/auth/login', body)
  // set cookie 
}

export const registerAction = (body: RegisterType) => {
  return http.post<RegisterSuccessResponse>('/auth/register', body)
}
