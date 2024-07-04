'use server'

import envConfig from '@/config'
import { UploadFileResponse } from '@/types/file.type'
import { ApiSuccessResponse } from '@/types/util.types'

export const uploadFileAction = async (body: FormData) => {
  const response = await fetch(`${envConfig.API_ENDPOINT}/files`, {
    method: 'POST',
    headers: {
      // 'Content-Type': 'multipart/form-data' is not needed;
    },
    body: body
  })

  if (!response.ok) {
    throw new Error('Failed to upload file')
  }

  const result = await response.json()
  return result as ApiSuccessResponse<UploadFileResponse>
}
