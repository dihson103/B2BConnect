import { toast } from '@/components/ui/use-toast'
import { ApiSuccessResponse } from '@/types/util.types'
import { type ClassValue, clsx } from 'clsx'
import { twMerge } from 'tailwind-merge'

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

export const apiErrorHandler = (error: string) => {
  console.log('>>>>> error', error)
  toast({
    title: 'Lỗi',
    description: error ?? 'Lỗi không xác định',
    duration: 5000,
    variant: 'destructive'
  })
}

export const apiResultHandler = (res: ApiSuccessResponse<null>) => {
  toast({
    title: res.status === 200 ? 'Thành công' : 'Lỗi',
    description: res.message ?? 'Lỗi không xác định',
    duration: 5000,
    variant: res.status === 200 ? 'default' : 'destructive'
  })
}

export function formatBytes(
  bytes: number,
  opts: {
    decimals?: number
    sizeType?: 'accurate' | 'normal'
  } = {}
) {
  const { decimals = 0, sizeType = 'normal' } = opts

  const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB']
  const accurateSizes = ['Bytes', 'KiB', 'MiB', 'GiB', 'TiB']
  if (bytes === 0) return '0 Byte'
  const i = Math.floor(Math.log(bytes) / Math.log(1024))
  return `${(bytes / Math.pow(1024, i)).toFixed(decimals)} ${
    sizeType === 'accurate' ? accurateSizes[i] ?? 'Bytest' : sizes[i] ?? 'Bytes'
  }`
}
