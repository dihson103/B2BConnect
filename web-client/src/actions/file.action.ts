'use server'

import action from '@/actions/common.action'
import envConfig from '@/config'
import { LoginResponse } from '@/types/auth.types'
import { ApiSuccessResponse } from '@/types/util.types'
import { cookies } from 'next/headers'

export const uploadFileAction = async (body: FormData) => {
  const loginResponse = cookies().get('loginResponse')?.value
  if (!loginResponse) throw new Error('Không có quyền xem')
  const token: LoginResponse = JSON.parse(loginResponse)
  const response = await fetch(`${envConfig.API_ENDPOINT}/files`, {
    method: 'POST',
    headers: {
      // 'Content-Type': 'multipart/form-data',
      Authorization: 'Bearer ' + token.accessToken
    },
    body: body
  })

  console.log('token', token.accessToken)

  if (!response.ok) {
    throw new Error('Failed to upload file')
  }

  const result = await response.json()
  return result as ApiSuccessResponse<string[]>
}
