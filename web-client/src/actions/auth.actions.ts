'use server'

import http from '@/lib/http'
import { cookies, headers } from 'next/headers'
import {
  LoginResponse,
  LoginSuccessResponse,
  LoginType,
  RegisterSuccessResponse,
  RegisterType
} from '@/types/auth.types'
import { redirect } from 'next/navigation'

export const loginAction = async (body: LoginType) => {
  let response
  try {
    response = await http.post<LoginSuccessResponse>('/auth/login', body)
    if (response.status === 200 && response.data) {
      cookies().set({
        name: 'loginResponse',
        value: JSON.stringify(response.data),
        httpOnly: false
      })
    } else {
      throw new Error('Login failed: Unexpected response')
    }
  } catch (error) {
    console.error('Login request failed:', error)
    throw error
  }
  return response
}

export const registerAction = (body: RegisterType) => {
  return http.post<RegisterSuccessResponse>('/auth/register', body)
}

export const getLoginResponseCookie = (): LoginResponse | null => {
  try {
    const loginResponse = cookies().get('loginResponse')?.value
    if (!loginResponse) {
      return null
    }
    return JSON.parse(loginResponse)
  } catch (err) {
    console.error('Error parsing loginResponse cookie:', err)
    return null
  }
}

export const handleAuthError = async () => {
  console.log('ddddddddddddd-=============')
  await fetch('http://localhost:3000/api/auth/test', {
    headers: {
      'Content-Type': 'application/json'
    }
  })
}

export const deleteAuthCookie = () => {}
