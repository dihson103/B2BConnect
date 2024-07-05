import { toast } from '@/components/ui/use-toast'
import { ApiSuccessResponse } from '@/types/util.types'
import { type ClassValue, clsx } from 'clsx'
import { twMerge } from 'tailwind-merge'

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

export const apiErrorHandler = (error: string) => {
  toast({
    title: 'Lỗi',
    description: error ?? 'Lỗi không xác định',
    duration: 5000,
    variant: 'destructive'
  })
}

export const apiResultHandler = (res: ApiSuccessResponse<null>) => {
  toast({
    title: res.isSuccess ? 'Thành công' : 'Lỗi',
    description: res.message ?? 'Lỗi không xác định',
    duration: 5000,
    variant: res.isSuccess ? 'default' : 'destructive'
  })
}
